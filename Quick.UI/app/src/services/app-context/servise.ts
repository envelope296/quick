import type { WebApp } from "@/models/web-app";

var userToken: string | null = null;

export function getWebApp(): WebApp {
    return window.WebApp;
}

export function getUserToken():  string | null {
    return userToken;
}

export function setUserToken(token: string) {
    userToken = token;
}