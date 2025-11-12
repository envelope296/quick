import { buildAxiosInstanse } from '@/services/builders'

const api = buildAxiosInstanse('/v1/users');

export async function getUserToken(webAppInitData: string): Promise<string> {
    const response = await api.post('/token', {
        initData: webAppInitData
    });
    return response.data;
}