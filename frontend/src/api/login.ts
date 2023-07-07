import request from "./request"

export interface LoginRequest {
    username: string,
    password: string
}

export default function login(loginRequest: LoginRequest): string | undefined {
    let token: string | undefined = undefined;
    request({
        url: '/Login/LoginByUserNameAndPwd',
        method: 'post',
        data: loginRequest
    }).then(res => {
        console.log(JSON.stringify(res))
    }).catch(err => {
        console.log(err)
    });
    return token;
}