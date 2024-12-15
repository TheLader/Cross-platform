import React, { useState, useEffect } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";

interface RegisterFormData {
    username: string;
    fullName: string;
    password: string;
    confirmPassword: string;
    phone: string;
    email: string;
}

const RegisterPage: React.FC = () => {
    const { loginWithRedirect } = useAuth0();
    const [formData, setFormData] = useState<RegisterFormData>({
        username: "",
        fullName: "",
        password: "",
        confirmPassword: "",
        phone: "",
        email: "",
    });
    const [errors, setErrors] = useState<{ [key: string]: string }>({});
    const [authToken, setAuthToken] = useState<string | null>(null);
    useEffect(() => {
        const fetchAuthToken = async () => {
            const domain = "dev-r617abi65lh733n3.us.auth0.com";
            const clientId = "cl28ztazFP3koY3gDgMfSpsDrIxcE974";
            const clientSecret = "kMCiGEibvTMkH4o-qtev4oPCbKYNbCimUxBNJYpqT4Pyn8YeTRKADyEHtEd9DVyk";

            if (!domain || !clientId || !clientSecret) {
                console.error("Перевірте конфігурацію .env файлу!");
                return;
            }

            try {
                const response = await axios.post(`https://${domain}/oauth/token`, {
                    client_id: clientId,
                    client_secret: clientSecret,
                    audience: `https://${domain}/api/v2/`,
                    grant_type: "client_credentials",
                });

                setAuthToken(response.data.access_token);
            } catch (err: any) {
                console.error("Error fetching token:", err.response?.data || err.message);
            }
        };

        fetchAuthToken();
    }, []);

    const validateForm = (): boolean => {
        const newErrors: { [key: string]: string } = {};

        if (!formData.username || formData.username.length > 50) {
            newErrors.username = "Ім'я користувача не може бути більше 50 символів.";
        }

        if (!formData.fullName || formData.fullName.length > 500) {
            newErrors.fullName = "ФІО не може бути більше 500 символів.";
        }

        if (!formData.password || formData.password.length < 8 || formData.password.length > 16) {
            newErrors.password = "Пароль має бути від 8 до 16 символів.";
        } else if (!/(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])/.test(formData.password)) {
            newErrors.password = "Пароль має містити хоча б 1 велику літеру, 1 цифру та 1 спеціальний символ.";
        }

        if (formData.password !== formData.confirmPassword) {
            newErrors.confirmPassword = "Паролі не співпадають.";
        }

        if (!/^\+380\d{9}$/.test(formData.phone)) {
            newErrors.phone = "Телефон має бути у форматі +380XXXXXXXXX.";
        }

        if (!/\S+@\S+\.\S+/.test(formData.email)) {
            newErrors.email = "Некоректна електронна адреса.";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        if (!validateForm()) return;

        if (!authToken) {
            console.error("Auth token not available!");
            return;
        }

        try {
            const domain = "dev-r617abi65lh733n3.us.auth0.com";
            await axios.post(
                `https://${domain}/api/v2/users`,
                {
                    connection: "Username-Password-Authentication",
                    email: formData.email,
                    password: formData.password,
                    user_metadata: {
                        username: formData.username,
                        fullName: formData.fullName,
                        phone: formData.phone,
                    },
                },
                {
                    headers: {
                        Authorization: `Bearer ${authToken}`,
                    },
                },
            );

            await loginWithRedirect();
        } catch (error) {
            console.error("Помилка реєстрації:", error);
        }
    };

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value });
    };

    return (
        <div className="container mt-5">
            <h2>Реєстрація</h2>
            <form onSubmit={handleSubmit}>
                {["username", "fullName", "email", "phone", "password", "confirmPassword"].map((field) => (
                    <div key={field} className="mb-3">
                        <label htmlFor={field} className="form-label">
                            {field === "confirmPassword"
                                ? "Підтвердьте пароль"
                                : field.charAt(0).toUpperCase() + field.slice(1)}
                        </label>
                        <input
                            type={field.includes("password") ? "password" : "text"}
                            className={`form-control ${errors[field] ? "is-invalid" : ""}`}
                            id={field}
                            name={field}
                            value={formData[field as keyof RegisterFormData]}
                            onChange={handleChange}
                        />
                        {errors[field] && <div className="invalid-feedback">{errors[field]}</div>}
                    </div>
                ))}
                <button type="submit" className="btn btn-primary" disabled={!authToken}>
                    Зареєструватися
                </button>
            </form>
        </div>
    );
};

export default RegisterPage;
