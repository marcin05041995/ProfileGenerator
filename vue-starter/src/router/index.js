import Vue from "vue";
import Router from "vue-router";
import Meta from "vue-meta";
import Employee from "@/components/Employee";
import Project from "@/components/Project";
import Card from "@/components/Card";
import AddEmployee from "@/components/AddEmployee";
import AddProject from "@/components/AddProject";
import EditEmployee from "@/components/EditEmployee";
import EditProject from "@/components/EditProject";

import globals from "@/globals";

// Layouts
import Layout1 from "@/layout/Layout1";

Vue.use(Router);
Vue.use(Meta);

const router = new Router({
  base: "/",
  mode: "history",
  routes: [
    {
      path: "/",
      component: Layout1,
      children: [
        {
          path: "employee",
          component: () => import("@/components/Employee")
        },
        {
          path: "project",
          component: () => import("@/components/Project")
        },
        {
          path: "card",
          component: () => import("@/components/Card")
        },
        {
          path: "addemployee",
          component: () => import("@/components/AddEmployee")
        },
        {
          path: "addproject",
          component: () => import("@/components/AddProject")
        },
        {
          path: "editemployee",
          component: () => import("@/components/EditEmployee")
        },
        {
          path: "editproject",
          component: () => import("@/components/EditProject")
        }
      ]
    }
  ]
});

router.afterEach(() => {
  // On small screens collapse sidenav
  if (
    window.layoutHelpers &&
    window.layoutHelpers.isSmallScreen() &&
    !window.layoutHelpers.isCollapsed()
  ) {
    setTimeout(() => window.layoutHelpers.setCollapsed(true, true), 10);
  }

  // Scroll to top of the page
  globals().scrollTop(0, 0);
});

router.beforeEach((to, from, next) => {
  // Set loading state
  document.body.classList.add("app-loading");

  // Add tiny timeout to finish page transition
  setTimeout(() => next(), 10);
});

export default router;
