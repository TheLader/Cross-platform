import React, { useState } from "react";
import { LogoutOptions, useAuth0 } from "@auth0/auth0-react";
import Home from "./Views/Home";
import BoardGame from "./Views/BoardGame";
import MaxProfit from "./Views/MaxProfit";
import GoGame from "./Views/GoGame";
import UserProfile from "./Views/UserProfile";
import RegisterPage from "./Views/RegisterPage";
// import Registration from "./Registration"; // Ваш компонент Registration

const App: React.FC = () => {
    const { loginWithRedirect, logout, isAuthenticated, user, isLoading } = useAuth0();
    const [selectedMenu, setSelectedMenu] = useState<string>("Home"); // Трекер вибраного меню

    const handleLogin = () => loginWithRedirect();
    const handleLogout = () => logout({ returnTo: window.location.origin } as LogoutOptions);

    if (isLoading) {
        return <div>Loading...</div>;
    }

    const renderComponent = () => {
        switch (selectedMenu) {
            case "Home":
                return <Home />;
            case "MaxProfit":
                return <MaxProfit />;
            case "GoGame":
                return <GoGame />;
            case "BoardGame":
                return <BoardGame />;
            case "Profile":
                return <UserProfile />;
            case "Register":
                return <RegisterPage />;
            default:
                return <Home />;
        }
    };

    return (
        <div>
            <header>
                <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                    <div className="container-fluid">
                        <div className="navbar-brand">Lab5</div>
                        <button
                            className="navbar-toggler"
                            type="button"
                            data-bs-toggle="collapse"
                            data-bs-target=".navbar-collapse"
                            aria-controls="navbarSupportedContent"
                            aria-expanded="false"
                            aria-label="Toggle navigation"
                        >
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                            <ul className="navbar-nav flex-grow-1">
                                <li className="nav-item">
                                    <button
                                        className={`btn nav-link text-dark ${selectedMenu === "Home" ? "active" : ""}`}
                                        onClick={() => setSelectedMenu("Home")}
                                    >
                                        Home
                                    </button>
                                </li>
                                {isAuthenticated ? (
                                    <>
                                        <li>
                                            <button
                                                className={`btn nav-link text-dark ${selectedMenu === "Profile" ? "active" : ""}`}
                                                onClick={() => setSelectedMenu("Profile")}
                                            >
                                                Hello {user?.name}!
                                            </button>
                                        </li>
                                        <li>
                                            <button
                                                className={`btn nav-link text-dark ${selectedMenu === "MaxProfit" ? "active" : ""}`}
                                                onClick={() => setSelectedMenu("MaxProfit")}
                                            >
                                                Max Profit
                                            </button>
                                        </li>
                                        <li>
                                            <button
                                                className={`btn nav-link text-dark ${selectedMenu === "GoGame" ? "active" : ""}`}
                                                onClick={() => setSelectedMenu("GoGame")}
                                            >
                                                Go Game
                                            </button>
                                        </li>
                                        <li>
                                            <button
                                                className={`btn nav-link text-dark ${selectedMenu === "BoardGame" ? "active" : ""}`}
                                                onClick={() => setSelectedMenu("BoardGame")}
                                            >
                                                Board Game
                                            </button>
                                        </li>
                                        <li>
                                            <button className="btn nav-link text-dark" onClick={handleLogout}>
                                                Logout
                                            </button>
                                        </li>
                                    </>
                                ) : (
                                    <>
                                        <li>
                                            <button className="btn nav-link text-dark" onClick={handleLogin}>
                                                Login
                                            </button>
                                        </li>
                                        <li>
                                            <button
                                                className={`btn nav-link text-dark ${selectedMenu === "Register" ? "active" : ""}`}
                                                onClick={() => setSelectedMenu("Register")}
                                            >
                                                Registration
                                            </button>
                                        </li>
                                    </>
                                )}
                            </ul>
                        </div>
                    </div>
                </nav>
            </header>
            <div className="container">
                <main role="main" className="pb-3">
                    {renderComponent()}
                </main>
            </div>
        </div>
    );
};

export default App;
