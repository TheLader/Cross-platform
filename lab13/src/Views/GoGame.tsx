import React, { useState } from "react";
import { GoGameUtils } from "../Labs/GoGameUtils";

interface GoGameViewModel {
    boardSize: number;
    boardLinesInput: string;
    groupsInAtari?: number | null;
}

const GoGame: React.FC = () => {
    const [model, setModel] = useState<GoGameViewModel>({
        boardSize: 0,
        boardLinesInput: "",
        groupsInAtari: null,
    });

    const [errors, setErrors] = useState<{ boardSize?: string; boardLinesInput?: string }>({});
    const [result, setResult] = useState<number | null>(null);

    const validate = (): boolean => {
        const newErrors: { boardSize?: string; boardLinesInput?: string } = {};

        if (model.boardSize < 1 || model.boardSize > 19) {
            newErrors.boardSize = "Розмір дошки повинен бути між 1 і 19.";
        }

        if (!model.boardLinesInput.trim()) {
            newErrors.boardLinesInput = "Введення рядків дошки не може бути порожнім.";
        } else {
            const lines = model.boardLinesInput.split("\n");
            if (lines.length !== model.boardSize) {
                newErrors.boardLinesInput = `Кількість рядків має дорівнювати ${model.boardSize}.`;
            } else if (
                !lines.every((line) => /^[B|W|.]+$/.test(line.trim()) && line.trim().length === model.boardSize)
            ) {
                newErrors.boardLinesInput =
                    "Кожен рядок повинен містити рівно стільки символів, скільки розмір дошки, і містити тільки 'B', 'W', або '.'.";
            }
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setModel((prev) => ({
            ...prev,
            [name]: name === "boardSize" ? Number(value) : value,
        }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (validate()) {
            const lines = model.boardLinesInput.split("\n").map((line) => line.trim());
            const board = lines.map((line) => line.split(""));
            const groups = GoGameUtils.countGroupsInAtari(board, model.boardSize);
            setModel((prev) => ({ ...prev, groupsInAtari: groups }));
            setResult(groups);
        }
    };

    return (
        <div className="container mt-4">
            <h2>Go Game Atari Checker</h2>

            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="boardSize">Розмір дошки (N):</label>
                    <input
                        type="number"
                        id="boardSize"
                        name="boardSize"
                        value={model.boardSize}
                        onChange={handleChange}
                        className="form-control"
                    />
                    {errors.boardSize && <span className="text-danger">{errors.boardSize}</span>}
                </div>

                <div className="form-group mt-3">
                    <label htmlFor="boardLinesInput">Лінії дошки (по одній на кожен рядок):</label>
                    <textarea
                        id="boardLinesInput"
                        name="boardLinesInput"
                        value={model.boardLinesInput}
                        onChange={handleChange}
                        className="form-control"
                        rows={8}
                    ></textarea>
                    {errors.boardLinesInput && <span className="text-danger">{errors.boardLinesInput}</span>}
                    <small className="form-text text-muted">
                        Введіть лінії дошки, розділені новими рядками. Літери мають бути &apos;B&apos;, &apos;W&apos;,
                        або &apos;.&apos;.
                    </small>
                </div>

                <button type="submit" className="btn btn-primary mt-3">
                    Перевірити групи в атарі
                </button>
            </form>

            {result !== null && (
                <div className="alert alert-success mt-3">
                    <strong>Групи в атарі:</strong> {result}
                </div>
            )}
        </div>
    );
};

export default GoGame;
