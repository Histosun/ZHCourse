import { lazy } from "react";
import { RouteObject } from "react-router-dom";
import Home from "../view/Home";
import Login from "../view/Login";

const Language = lazy(() => import("../view/Language"));

export const HOME_PATH = "/home";
export const LISTENTING_PATH = "/listening";
export const LOGIN_PATH = "/login";

export const routes = [
    {
        path: '/',
        element: <Home />
    },
    {
        path: HOME_PATH,
        element: <Home />
    },
    {
        path: LISTENTING_PATH + "/:language",
        element: <Language />
    },
    {
        path: LOGIN_PATH,
        element: <Login />
    }
] as RouteObject[];