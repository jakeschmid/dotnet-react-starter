import axios from 'axios';

export interface NoteResponse {
  id: string;
  title: string;
  content: string;
  createdAt: string;
}

export interface CreateNoteRequest {
  title: string;
  content: string;
}

export interface UpdateNoteRequest {
  title: string;
  content: string;
}

const api = axios.create({
  baseURL: `${import.meta.env.VITE_API_BASE_URL}/api`,
  headers: { 'Content-Type': 'application/json' },
});

export const notesApi = {
  getAll: () => api.get<NoteResponse[]>('/notes').then((r) => r.data),

  getById: (id: string) =>
    api.get<NoteResponse>(`/notes/${id}`).then((r) => r.data),

  create: (data: CreateNoteRequest) =>
    api.post<NoteResponse>('/notes', data).then((r) => r.data),

  update: (id: string, data: UpdateNoteRequest) =>
    api.put<NoteResponse>(`/notes/${id}`, data).then((r) => r.data),

  delete: (id: string) => api.delete(`/notes/${id}`),
};
