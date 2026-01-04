import api from './api';

export const memberService = {
  getAll: async () => {
    const response = await api.get('/members');
    return response.data;
  },

  getById: async (id) => {
    const response = await api.get(`/members/${id}`);
    return response.data;
  },

  create: async (memberData) => {
    const response = await api.post('/members', memberData);
    return response.data;
  },

  update: async (id, memberData) => {
    const response = await api.put(`/members/${id}`, memberData);
    return response.data;
  },

  delete: async (id) => {
    const response = await api.delete(`/members/${id}`);
    return response.data;
  },

  getActiveMembers: async () => {
    const response = await api.get('/members/active');
    return response.data;
  },
};
