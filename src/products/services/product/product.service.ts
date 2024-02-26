import { Injectable } from "@nestjs/common";
import { CreateProduct, Product } from "src/products/models";

@Injectable()
export class ProductService {
  constructor() {
    this.products.push({
      id: 1708966378040,
      name: "Apple",
      price: 100,
      imageUrl: "./assets/images/img1.jpg",
    });
  }
  private readonly products: Product[] = [];

  create(product: CreateProduct): Product {
    const item = { ...product, id: new Date().getTime() };
    this.products.push(item);
    return item;
  }

  findAll(): Product[] {
    return this.products;
  }
}
