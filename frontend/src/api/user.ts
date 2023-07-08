import request from "./request"

export interface LoginRequest {
    username: string,
    password: string
}

export function login(loginRequest: LoginRequest) {
    return request({
        url: '/Login/LoginByUserNameAndPwd',
        method: 'post',
        headers: {
            isToken: false
        },
        data: loginRequest
    });
}

export function logout() {
}