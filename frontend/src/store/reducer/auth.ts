import { Reducer } from 'redux'

interface AuthState {
    token: string|undefined;
}

const baseReduer: Reducer<AuthState> = (state = { token: undefined }, action) => {
    switch(action.type){
        default:
    }

    return state;
}

export default baseReduer;