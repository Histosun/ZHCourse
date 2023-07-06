import { lazy } from "react";
import { RouteObject } from "react-router-dom";
import Home from "../view/Home";

const Language = lazy(() => import("../view/Language"));

export const routes = [
    {
        path: '/',
        element: <Home />
    },
    {
        path: '/home',
        element: <Home />
    },
    {
        path: '/listening/:language',
        element: <Language />
    }
] as RouteObject[];