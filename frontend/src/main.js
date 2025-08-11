import './assets/app.css'
import './assets/bootstrap.min.css'
// import './assets/UI.styles.css'

import {createApp} from 'vue'
import {createPinia} from 'pinia'

import App from './App.vue'

const app = createApp(App)

app.use(createPinia())
// app.use(router)

app.mount('#app')
