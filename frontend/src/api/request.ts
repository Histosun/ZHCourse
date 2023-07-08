import axios, { InternalAxiosRequestConfig } from "axios"
import util from "../util";

axios.defaults.headers['Content-Type'] = 'application/json;charset=utf-8'

const baseUrl: string = "https://localhost:7159";

let request = axios.create({
    baseURL: baseUrl,
    timeout: 5000
});

request.interceptors.request.use(
    (config: InternalAxiosRequestConfig) => {
        if (config.headers.isToken) {
            let token = util.cookie.getToken();
            if (!token) throw new Error('User not authenticated');
            config.headers['Authentication'] = token;
        }
        return config;
    },
    err => {
        console.log(err);
        Promise.reject(err);
    }
);

request.interceptors.response.use(
    res => {
        return res;
    },
    err => {
        return Promise.reject(err)
    }
);

export default request;