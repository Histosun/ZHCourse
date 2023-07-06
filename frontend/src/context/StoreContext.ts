import React from "react"
import { Store } from "redux"
import store from "../store"

export default React.createContext<Store>(store);