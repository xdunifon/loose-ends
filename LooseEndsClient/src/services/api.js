import axios from 'axios'

const apiClient = axios.create({
  baseURL: 'https://localhost:44300/api',
  withCredentials: false,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  },
  timeout: 10000,
})

// Request Interceptor
apiClient.interceptors.request.use(
  (config) => {
    // Add headers or modify request before sending
    return config
  },
  (error) => {
    // Handle request errors
    console.error('Request Error:', error)
    return Promise.reject(error)
  },
)

// Response Interceptor
apiClient.interceptors.response.use(
  (response) => {
    // Handle successful response
    return response
  },
  (error) => {
    // Handle errors globally
    if (error.response) {
      const { status, data } = error.response

      console.error(`API Error [${status}]:`, data)
    } else if (error.request) {
      console.error('No Response Received:', error.request)
    } else {
      console.error('Error:', error.message)
    }

    return Promise.reject(error)
  },
)

export default apiClient
