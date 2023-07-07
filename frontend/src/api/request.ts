import axios, { InternalAxiosRequestConfig } from "axios"

axios.defaults.headers['Content-Type'] = 'application/json;charset=utf-8'

interface AxiosConfigData {
    isToken: boolean;
}

const baseUrl: string = "https://localhost:7159";

let request = axios.create({
    baseURL: baseUrl,
    timeout: 5000
});

request.interceptors.request.use(
    (config: InternalAxiosRequestConfig<AxiosConfigData>) => {
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
        console.log(123);
        return Promise.reject(err)
    }
);

export default request;