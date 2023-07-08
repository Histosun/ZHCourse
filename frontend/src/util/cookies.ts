import Cookies from 'js-cookie'

const token_key = 'user_token'

function setToken(token: string) {
    Cookies.set(token_key, token);
}

function getToken(): string | undefined {
    return Cookies.get(token_key);
}

function removeToken() {
    Cookies.remove(token_key);
}

export default {
    setToken: setToken,
    getToken: getToken,
    removeToken: removeToken
}