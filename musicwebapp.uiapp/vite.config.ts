import { defineConfig } from "vite";

export default defineConfig({
  server: {
    host: "0.0.0.0", // Accept connections from outside the container
    port: 5173, // Ensure it matches the exposed port
  },
});
