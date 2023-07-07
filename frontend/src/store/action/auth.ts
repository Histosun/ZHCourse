import * as TYPES from "../action_types"

export default {
    login() {
        return {
            type: TYPES.AUTH_LOGIN
        }
    },
    logout() {
        return {
            type: TYPES.AUTH_LOGOUT
        }
    }
}