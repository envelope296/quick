import { AppConfig } from "@/config/app-config";
import axios, { type AxiosInstance } from "axios";
import * as appContext from '@/services/app-context'

export function buildAxiosInstanse(relativeUrl: string, useAuthorization: boolean = false): AxiosInstance {
    const headers: Record<string, string> = {
        'Content-Type': 'application/json'
    }
    if (useAuthorization) {
        const token = appContext.getUserToken();
        if (token !== null) {
            headers['Authorization'] = `Bearer ${token}`;
        }
    }
    
    const api = axios.create({
        baseURL: `${AppConfig.baseApiUrl}${relativeUrl}`,
        headers: headers
    });
    return api;
}