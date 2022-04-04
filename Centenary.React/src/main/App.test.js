import { render } from '@testing-library/react';
import App from './App';

test("Renders an H1", () => {
    const { getByText } = render(<App />);
    const h1 = getByText(/Hello/);
    expect(h1).toHaveTextContent("Hello");
});
