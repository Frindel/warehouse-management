import {createRouter, createWebHistory} from 'vue-router'
import receipts from '@/pages/receipts.vue'
import Units from "@/pages/units.vue";
import Resources from "@/pages/resources.vue";
import UnitsForm from "@/pages/units-form.vue";
import ResourceForm from "@/pages/resource-form.vue";
import ReceiptForm from "@/pages/receipt-form.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {path: '/', redirect: {name: 'receipts'}},
        {path: '/units/', component: Units, name: 'units'},
        {path: '/units/new', component: UnitsForm, name: 'create-unit', props: {mode: 'create'}},
        {
            path: '/units/:id/edit',
            component: UnitsForm,
            name: 'edit-unit',
            props: route => ({mode: 'edit', id: route.params.id})
        },
        {path: '/resources/', component: Resources, name: 'resources'},
        {path: '/resources/new', component: ResourceForm, name: 'create-resource', props: {mode: 'create'}},
        {
            path: '/resources/:id/edit',
            component: ResourceForm,
            name: 'edit-resource',
            props: route => ({mode: 'edit', id: route.params.id})
        },
        {path: '/receipts/', component: receipts, name: 'receipts'},
        {path: '/receipts/new', component: ReceiptForm, name: 'create-receipt', props: {mode: 'create'}},
        {
            path: '/receipts/:id/edit',
            component: ReceiptForm,
            name: 'edit-receipt',
            props: route => ({mode: 'edit', id: route.params.id})
        },
    ],
    linkActiveClass: 'active',
    linkExactActiveClass: 'active',
})

export default router