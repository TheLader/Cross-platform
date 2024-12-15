import React, { useState } from "react";
import { BoardGameUtils } from "../Labs/BoardGameUtils";

interface BoardGameViewModel {
    m: number;
    n: number;
    result?: number | null;
}

const BoardGame: React.FC = () => {
    const [model, setModel] = useState<BoardGameViewModel>({ m: 1, n: 1, result: null });
    const [errors, setErrors] = useState<{ m?: string; n?: string }>({});
    const [result, setResult] = useState<string | null>(null);

    const validate = (): boolean => {
        const newErrors: { m?: string; n?: string } = {};

        if (model.m < 1 || model.m > 250) {
            newErrors.m = "Координати m повинні бути в діапазоні від 1 до 250.";
        }

        if (model.n < 1 || model.n > 250) {
            newErrors.n = "Координати n повинні бути в діапазоні від 1 до 250.";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setModel((prev) => ({
            ...prev,
            [name]: Number(value),
        }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (validate()) {
            try {
                const result = BoardGameUtils.getResult(model.m, model.n);
                setModel((prev) => ({ ...prev, result: result }));
                setResult(result === 1 ? "Перша позиція виграє" : "Друга позиція виграє");
            } catch (error) {
                console.error("Помилка під час обчислення результату", error);
                setResult("Помилка під час обчислення результату");
            }
        }
    };

    return (
        <div className="container mt-4">
            <h2>Board Game Result Checker</h2>

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="m">Координата m:</label>
                    <input
                        type="number"
                        id="m"
                        name="m"
                        value={model.m}
                        onChange={handleChange}
                        className="form-control"
                    />
                    {errors.m && <span className="text-danger">{errors.m}</span>}
                </div>

                <div className="form-group mt-3">
                    <label htmlFor="n">Координата n:</label>
                    <input
                        type="number"
                        id="n"
                        name="n"
                        value={model.n}
                        onChange={handleChange}
                        className="form-control"
                    />
                    {errors.n && <span className="text-danger">{errors.n}</span>}
                </div>

                <button type="submit" className="btn btn-primary mt-3">
                    Отримати результат
                </button>
            </form>

            {result && (
                <div className="alert alert-success mt-3">
                    <strong>Результат:</strong> {result}
                </div>
            )}
        </div>
    );
};

export default BoardGame;
