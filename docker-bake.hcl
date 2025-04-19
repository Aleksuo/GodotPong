group "default" {
  targets = ["godotbase"]
}

variable "TAG" {
  default = "latest"
}

target "godotbase" {
  dockerfile = "Dockerfile"
  tags = ["godotpong/godotbase:${TAG}"]
}