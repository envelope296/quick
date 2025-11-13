import type { WebApp } from "@/models/web-app";

export function getWebApp(): WebApp {
    return window.WebApp;
}