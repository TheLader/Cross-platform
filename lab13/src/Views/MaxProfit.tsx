import React, { useState } from "react";
import { ProfitUtils } from "../Labs/ProfitUtils";

interface MaxProfitViewModel {
    pricesCount: number;
    pricesInput: string;
    result?: number | null;
}

const MaxProfit: React.FC = () => {
    const [model, setModel] = useState<MaxProfitViewModel>({
        pricesCount: 0,
        pricesInput: "",
        result: null,
    });

    const [errors, setErrors] = useState<{ pricesCount?: string; pricesInput?: string }>({});
    const [result, setResult] = useState<number | null>(null);

    const validate = (): boolean => {
        const newErrors: { pricesCount?: string; pricesInput?: string } = {};

        if (model.pricesCount <= 0) {
            newErrors.pricesCount = "Кількість цін повинна бути більше нуля.";
        }

        if (!model.pricesInput.trim()) {
            newErrors.pricesInput = "Поле цін не може бути порожнім.";
        } else {
            const prices = model.pricesInput.split(" ").map((price) => parseInt(price, 10));
            if (prices.length !== model.pricesCount || prices.some(isNaN)) {
                newErrors.pricesInput = "Введіть коректну кількість чисел, розділених пробілами.";
            }
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setModel((prev) => ({
            ...prev,
            [name]: name === "pricesCount" ? Number(value) : value,
        }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (validate()) {
            const prices = model.pricesInput.split(" ").map((price) => parseInt(price, 10));
            const maxProfit = ProfitUtils.getMaxProfit(prices);
            setModel((prev) => ({ ...prev, result: maxProfit }));
            setResult(maxProfit);
        }
    };

    return (
        <div className="container mt-4">
            <h2>Max Profit Calculator</h2>

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="pricesInput">Ціни (розділені пробілами):</label>
                    <input
                        type="text"
                        id="pricesInput"
                        name="pricesInput"
                        value={model.pricesInput}
                        onChange={handleChange}
                        className="form-control"
                    />
                    {errors.pricesInput && <span className="text-danger">{errors.pricesInput}</span>}
                </div>

                <div className="form-group mt-3">
                    <label htmlFor="pricesCount">Кількість цін:</label>
                    <input
                        type="number"
                        id="pricesCount"
                        name="pricesCount"
                        value={model.pricesCount}
                        onChange={handleChange}
                        className="form-control"
                    />
                    {errors.pricesCount && <span className="text-danger">{errors.pricesCount}</span>}
                </div>

                <button type="submit" className="btn btn-primary mt-3">
                    Обчислити
                </button>
            </form>

            {result !== null && (
                <div className="alert alert-success mt-3">
                    <strong>Максимальний прибуток:</strong> {result}
                </div>
            )}
        </div>
    );
};

export default MaxProfit;
