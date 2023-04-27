import axios from "axios";
import Axios, { AxiosRequestConfig } from "axios";
import qs from "qs";

export const BASE_URL = process.env.REACT_APP_BACKEND_URL ?? 'https://localhost:7123/api';
export const TOKEN = process.env.REACT_APP_CLIENTE_ID ?? 'YXNkYXNmcWVncTN5MmUyN3kyNmV3eXdleXczdWk0cmV1aXJv';

type AuthData = {
    message: string,
    success: boolean,
    token: string,
    tokenExpires: string
}

type FormLoginData = {
    email: string;
    password: string;
}

type CreateData = {
    email: string;
    password: string;
}

export const saveAuthData = (obj: AuthData) => {
    localStorage.setItem('authData', JSON.stringify(obj))
}

export const getAuthData = () => {
    const str = localStorage.getItem("authData") ?? "{}";
    return JSON.parse(str) as AuthData;
}

export const requestBackendLogin = (loginData: FormLoginData) => {
    const headers = {
        Authorization: `Bearer ${getAuthData}`
    }
    return Axios({ method: 'POST', baseURL: BASE_URL, url: '/orphanage/toggle-orphanage', headers })
}

export const requestBackend = (config: AxiosRequestConfig) => {
    const headers = config.withCredentials ? {
        ...config.headers,
        Authorization: `Bearer ` + getAuthData().token
    } : config.headers
    return axios({ ...config, baseURL: BASE_URL, headers });
}

export const requestBackendCreateOrphanage = (createData: CreateData) => {

    const data = qs.stringify({
        ...createData
    })
    return Axios({ method: 'POST', baseURL: BASE_URL, url: '/orphanages/insert', data })
}