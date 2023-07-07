import request from "./request"

export function login(): string|undefined {
    let token:string|undefined = undefined;
    request.post("")
        .then(() => {
            
        })
        .catch(() => {

        });
    return token;
}