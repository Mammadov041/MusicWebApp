# Step 1: Use a Node.js image
FROM node:18

# Step 2: Set the working directory
WORKDIR /app

# Step 3: Copy package.json and log contents for debugging
COPY package*.json ./
RUN ls -la /app

# Step 4: Install dependencies
RUN npm install

# Step 5: Copy the rest of the application code
COPY . .

# Step 6: Expose the port for Vite
EXPOSE 5173

# Step 7: Start the Vite dev server
CMD ["npm", "run", "dev"]
