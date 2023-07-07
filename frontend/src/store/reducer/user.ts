import { Reducer } from 'redux'
import * as TYPES from '../action_types'

interface UserState {
    token: string;
}

const baseReduer: Reducer<UserState> = (state = { token: '' }, action) => {
    switch (action.type) {
        case TYPES.USER_LOGIN:
            return { token: action.token }
        case TYPES.USER_LOGOUT:
            return { token: '' }
    }
    return { token: '' }
}

export default baseReduer;