class MainApi {
    getShelves = async () => {
        const resp = await fetch("/main/getshelves");
        return resp.json();
    }

    startOrders = () => {
        return fetch("/main/startorders");
    }

    stopOrders = () => {
        return fetch("/main/stoporders");
    }
}
  
export default new MainApi();