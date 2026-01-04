import { create } from 'zustand';
import { memberService } from '../services/memberService';

export const useMemberStore = create((set, get) => ({
  members: [],
  loading: false,
  error: null,

  fetchMembers: async () => {
    set({ loading: true, error: null });
    try {
      const members = await memberService.getAll();
      set({ members, loading: false });
    } catch (error) {
      set({ error: error.message, loading: false });
    }
  },

  createMember: async (memberData) => {
    set({ loading: true, error: null });
    try {
      const newMember = await memberService.create(memberData);
      set((state) => ({
        members: [...state.members, newMember],
        loading: false,
      }));
      return newMember;
    } catch (error) {
      set({ error: error.message, loading: false });
      throw error;
    }
  },

  updateMember: async (id, memberData) => {
    set({ loading: true, error: null });
    try {
      const updatedMember = await memberService.update(id, memberData);
      set((state) => ({
        members: state.members.map((m) => (m.id === id ? updatedMember : m)),
        loading: false,
      }));
      return updatedMember;
    } catch (error) {
      set({ error: error.message, loading: false });
      throw error;
    }
  },

  deleteMember: async (id) => {
    set({ loading: true, error: null });
    try {
      await memberService.delete(id);
      set((state) => ({
        members: state.members.filter((m) => m.id !== id),
        loading: false,
      }));
    } catch (error) {
      set({ error: error.message, loading: false });
      throw error;
    }
  },
}));
