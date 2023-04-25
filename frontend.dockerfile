FROM node:18.16.0

RUN mkdir -p /app

WORKDIR /app

COPY Frontend/package.json ./

RUN npm install

COPY Frontend/public/ ./public
COPY Frontend/src/ ./src

RUN npm run build

EXPOSE $PORT

ENV NUXT_HOST=0.0.0.0

ENV NUXT_PORT=$PORT

ENV REACT_APP_ASP_LINK=$REACT_APP_ASP_LINK

ENV PROXY_LOGIN=$PROXY_LOGIN

CMD [ "npm", "start" ]