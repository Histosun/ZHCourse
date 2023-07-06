import axios, { InternalAxiosRequestConfig } from "axios"

axios.defaults.headers['Content-Type'] = 'application/json;charset=utf-8'

interface AxiosConfigData {
    isToken: boolean;
}

const baseUrl: string = "http://localhost:8080";

let request = axios.create({
    baseURL: baseUrl,
    timeout: 5000
});

request.interceptors.request.use(
    (config: InternalAxiosRequestConfig<AxiosConfigData>) => {
        if (config.data?.isToken && ) {
            
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