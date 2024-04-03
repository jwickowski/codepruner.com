import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { Home } from "./Home";
import { SignalR01Processing } from "./pages/SignalR-01-Processing";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/signalR-01",
    element: <SignalR01Processing />,
  },
]);

export const App = () => {
  return (
    <>
      <nav>
        <ul>
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/signalR-01">SignalR example 1 : Processing</a>
          </li>
        </ul>
      </nav>
      <RouterProvider router={router} />
    </>
  );
};
