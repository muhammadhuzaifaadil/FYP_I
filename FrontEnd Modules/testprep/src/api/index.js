import axios from 'axios'

export const BASE_URL = 'https://localhost:7206';

export const ENDPOINTS = {
    question:'question',
    getAnswers : 'question/getanswers'
}

export const createAPIEndpoint = endpoint => {

    let url = `${BASE_URL}/api/${endpoint}/`; // Added a forward slash ("/") after BASE_URL
    return {
        fetch: () => axios.get(url),
        fetchById: id => axios.get(url + id),
        post: newRecord => axios.post(url, newRecord),
        put: (id, updatedRecord) => axios.put(url + id, updatedRecord),
        delete: id => axios.delete(url + id),
    }
}