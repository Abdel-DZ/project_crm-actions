import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    outDir: "../server/wwwroot/"

  },
  server: {
    port: 4000,
    proxy: {
      "/api": "http://localhost:3000/"
    }
  }
})