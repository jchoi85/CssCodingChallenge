# Description

This is a project demonstrating a sample implementation of a food ordering system for a coding challenge.

## How to Run

Simply clone the repository and then run `docker-compose up -d`.

Once the docker container is running, you should be able to access the front-end at http://localhost:5000. The buttons can be used to start or stop the order flow at any time.

## Design Considerations

The architecture is a loosely microservice based architecture. While the solution only contains one running service with multiple projects, each project has been completely segregated from each other. All services speak to each other asynchronously using a `IMessagingService` interface, which is a stand-in for any asynchronous messaging service such as Kafka or RabbitMQ. All synchronous communication between services are handled using a service-specific interface, which can be easily replaced with another synchronous communication protocol such as HTTP or gRPC, or refactored to be asynchronous.

The microservices are split up into different domains (drivers, orders, shelves) and Domain Driven Design (DDD) was used within the services. This allows the domain models to carry more of the business complexity within them, allowing them to be more flexible and encapsulated. I thought this was important for this task, as a complex system like this may add new services that need to work with these domains, and doing DDD should hopefully allow for these new use cases to be implemented with minimal domain refactoring.

Within the services themselves, I also used the mediator pattern to handle events. This allows the business logic for each event to be encapsulated into its own handler, which makes it easier to change how different events are handled while reducing the risk of breaking other workflows.

I used long-running tasks running once every second to handle the continuous aspects of the project, such as reading orders and monitoring the shelves. I thought that this one second timestep struck a fair balance between excessive CPU cycles keeping the shelves and orders up to date. The order service uses a long-running task to generate X number of orders per second based on a Poisson Distribution with a Lambda of 3.25 deliveries, and emits events every time an order is created. The Driver service hears this message, and 'dispatches' a driver to arrive between 2-10 seconds later to pick up the order. The Shelf service also hears the same message, and adds the order to the appropriate/available shelf. When the driver arrives, a message is emitted which tells the shelf service that the order has been picked up, and the order can be removed from its shelf. The shelf service also has a long-running task which constantly updates the value of each order in the shelf, removes any expired orders, and transfers any overflow orders to their respective shelves if possible.

I took a very basic approach to deciding how to move orders from the overflow shelf to their respective shelves. At each tick, the service tries to transfer any orders from overflow to the desired shelf on a first-come first-serve basis. This can lead to sub-optimal transferring, as there may be items closer to expiring that should be transferred first. The algorithm also tries to transfer ALL overflow orders per tick, regardless of the status of the other walls, which can lead to unnecessary CPU cycles. A possible improvement here would be to do a comparison in the available shelf capacity for each type, and then only attempt to transfer the number of orders necessary.

To implement the decay strategy, I used the strategy design pattern. My implementation created an interface called `IDecayStrategy`, which I then created different implementations of to represent both the normal decay rate as well as the overflow decay rate. This allowed me to initialize each shelf with a different decay strategy, while still allowing for the base `Shelf` class to handle the decay in a generic manner. Using this design pattern, additional decay strategies can be easily implemented and tested, and allows for great flexibility in changing what decay strategy is used for what wall at any given time.

## Improvements

As this was simply a coding challenge, I consciously made some sub-optimal design decisions to reduce the complexity of the system. If this were a real project, here's what I would have done differently/improved upon:

 **1. Improve overflow transfer algorithm -** The current algorithm to transfer overflow orders is very basic, and can be optimized. The most obvious optimization is to transfer orders closest to expiring, as to try and minimize the amount of order waste. Also, instead of trying to transfer all overflow orders, the algorithm can be a little more intelligent in how many orders it tries to transfer at a time based on the available shelf capacity.
 
 **2. Separating into actual microservices and implement async messaging (Kafka) -** This would allow for easier horizontal scaling of each individual service as necessary.
 
 **3. Implement a database to track shelves/orders -** Having a central database to store the state of the system would be more resilient to failure, and allow horizontally scaling of services.
 
 **4. Improve concurrency handling -** The current architecture uses locks, which do not work if there's more than one instance of a pod. To address this in a production system, I would begin by implementing a database as above. I would then use optimistic concurrency using a `[ConcurrencyCheck]` attribute on the shelves to ensure that multiple pods/threads can't try to edit the same shelf at the same time. Finally, with Kafka implemented, I would ensure make each message key the unique id of shelf the message is for. This would ensure that regardless of how many instances I have running of each service, all messages for one shelf will always be handled by the same instance.
 
 **5. Improved test suite -** In addition to some more comprehensive Unit testing, I would also introduce Integration testing to test each individual service from end-to-end.
 
 **6. Front-end Improvements -** The front-end is completely barebones. In addition to sprucing up the looks of the front-end, I would replace the `setInterval` with websockets (SignalR) to reload the page dynamically as orders come in, rather than refreshing the data every second.
