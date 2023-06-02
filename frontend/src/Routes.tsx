import { RouteObject } from "react-router-dom";
import Album from "./component/Album";

export default [
    {
        path: '/',
        element: <Album/>
    }
] as RouteObject[]