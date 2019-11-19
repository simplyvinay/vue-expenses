let required = (property) => {
  return v => !!v || `${property} is required`
}

let email = () => {
  return v => v && /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(v) || "Must be a valid email"
}

let minValue = (property, minValue) => {
  return v => (v && parseFloat(v) >= minValue) || `${property} must be greater than ${minValue}`
}

let maxValue = (property, maxValue) => {
  return v => (v && parseFloat(v) <= maxValue) || `${property} must be less than ${maxValue}`
}

let minLength = (property, minLength) => {
  return v => (v && v.length >= minLength) || `${property} must be greater than ${minLength} characters`
}

let maxLength = (property, maxLength) => {
  return v => (v && v.length <= maxLength) || `${property} must be less than ${maxLength} characters`
}

export default {
  required,
  email,
  minValue,
  maxValue,
  minLength,
  maxLength
}