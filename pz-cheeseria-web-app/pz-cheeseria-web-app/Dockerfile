# Stage 1: Build the Vue.js app
FROM node:16 AS build
WORKDIR /app

# Copy package.json and package-lock.json (if it exists) to install dependencies
COPY package*.json ./
# Debug step to check contents of /app after copying
RUN ls -la /app

RUN npm install

# Copy the rest of your application files
COPY . ./

# Build the Vue.js application
RUN npm run build

# Stage 2: Serve the Vue.js app using Nginx
FROM nginx:alpine

# Copy the built application from the build stage to Nginx's default HTML directory
COPY --from=build /app/dist /usr/share/nginx/html

# Expose port 80 for the Nginx server
EXPOSE 80

# Run Nginx in the foreground
CMD ["nginx", "-g", "daemon off;"]
