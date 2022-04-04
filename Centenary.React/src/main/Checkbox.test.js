import { render, fireEvent } from "@testing-library/react";
import { Checkbox } from "./Checkbox";

test("Selecting checkbox", () => {
    const { getByLabelText } = render(<Checkbox />);
    const checkbox = getByLabelText(/Not checked/);
    fireEvent.click(checkbox);
    expect(checkbox).toBeChecked();
});