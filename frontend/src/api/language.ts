import request from "./request"

export interface LoginRequest {
    username: string,
    password: string
}

export function getIndexByLanguage(languageId: string) {
    return request({
        url: '/Language/GetIndexByLanguage',
        params: {
            languageId: languageId
        },
        method: 'get',
        headers: {
            isToken: false
        }
    });
}