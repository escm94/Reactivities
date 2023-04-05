import { RouteObject } from 'react-router';
import { createBrowserRouter, Navigate } from 'react-router-dom';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import ActivityDetails from '../../features/activities/details/ActivityDetails';
import ActivityForm from '../../features/activities/form/ActivityForm';
import NotFound from '../../features/errors/NotFound';
import ServerError from '../../features/errors/ServerError';
import TestErrors from '../../features/errors/TestErrors';
import App from '../layout/App';
import LoginForm from '../../features/users/LoginForm';

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      { path: 'activities', element: <ActivityDashboard /> },
      { path: 'activities/:id', element: <ActivityDetails /> },
      { path: 'createActivity', element: <ActivityForm key="create" /> },
      { path: 'manage/:id', element: <ActivityForm key="manage" /> },
      { path: 'login', element: <LoginForm /> },
      { path: 'errors', element: <TestErrors key="manage" /> },
      { path: 'not-found', element: <NotFound key="manage" /> },
      { path: 'server-error', element: <ServerError key="manage" /> },
      { path: '*', element: <Navigate replace to="/not-found" /> },
    ],
  },
];

export const router = createBrowserRouter(routes);
