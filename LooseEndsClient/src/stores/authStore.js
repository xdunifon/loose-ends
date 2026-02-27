import { ref } from 'vue'
import { defineStore } from 'pinia'
import { cookieUtil } from '@/utils/cookieUtil'

export const useAuthStore = defineStore('auth', () => {
  const tokenCookie = cookieUtil.getCookie('session')
  const token = ref(tokenCookie)

  const setToken = (newToken) => {
    token.value = newToken
    document.cookie = `session=${newToken}; max-age=${60 * 60}; path=/` // One hour in seconds
  }

  return {
    token,
    setToken,
  }
})
