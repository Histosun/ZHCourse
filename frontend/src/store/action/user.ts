import * as TYPES from "../action_types"

export default {
    login(token: string) {
        return {
            type: TYPES.USER_LOGIN,
            token: token
        }
    },
    logout() {
        return {
            type: TYPES.USER_LOGOUT
        }
    }
}