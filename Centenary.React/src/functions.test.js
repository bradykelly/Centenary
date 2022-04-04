import {multiplyByTwo} from './functions.js';

test("Multiplies two numbers", () => {
  expect(multiplyByTwo(3)).toBe(6);
});