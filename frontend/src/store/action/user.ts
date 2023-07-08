import * as TYPES from "../action_types"

const user = {
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

export default user;