import type { WebApp } from '@/models/web-app';

declare global {
  interface Window {
    WebApp: WebApp
  }
}

export {};