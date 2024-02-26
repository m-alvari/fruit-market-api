import { Body, Controller, Get, Post } from "@nestjs/common";
import { CreateProduct, Product } from "./models";
import { ProductService } from "./services/product/product.service";

@Controller("products")
export class ProductsController {
  constructor(private readonly productService: ProductService) {}

  @Get()
  get(): Product[] {
    return this.productService.findAll();
  }

  @Post()
  create(@Body() createProduct: CreateProduct) {
    const item = this.productService.create(createProduct);
    return item;
  }
}
