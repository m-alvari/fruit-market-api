import type { Product } from ".";

export interface CreateProduct extends Omit<Product, "id"> {}
