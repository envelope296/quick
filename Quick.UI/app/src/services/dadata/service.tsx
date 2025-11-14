import type { SuggestionsResponse } from "@/models/datata/suggestion";
import axios from "axios";

const token = "76a66c1cccc609056e043ac5105c0dcd908f85a6";
const api = axios.create({
    baseURL: "https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/party",
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json",
        "Authorization": `Token ${token}`
    }
})

export async function suggestOrganizations(query: string, okved: string[]): Promise<SuggestionsResponse> {
    try {
        const response = await api.post<SuggestionsResponse>("", {
            query,
            okved
        })
        return response.data;
    }
    catch {
        return {
            suggestions: []
        }
    }
}

export async function suggestUniversities(query: string): Promise<SuggestionsResponse> {
    const response = await suggestOrganizations(query, ["85.22"]);
    return response;
}