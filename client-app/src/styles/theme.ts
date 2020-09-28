import { getLuminance, hsl, hsla } from "polished";

export type Color = Variant | "white" | "black";

export type ColorContrast =
  | "lightContrast"
  | "darkContrast"
  | "primaryContrast"
  | "secondaryContrast"
  | "tertiaryContrast"
  | "infoContrast"
  | "successContrast"
  | "warningContrast"
  | "dangerContrast";

export type Shade = "grayDarker" | "grayDark" | "gray" | "grayLight" | "grayLighter" | "grayLightest";

export type Variant =
  | "light"
  | "dark"
  | "primary"
  | "secondary"
  | "tertiary"
  | "info"
  | "success"
  | "warning"
  | "danger";

export type ViewportBreakpoint = "small" | "medium" | "large" | "extraLarge";

interface Theme
  extends Record<Color, string>,
    Record<ColorContrast, string>,
    Record<Shade, string>,
    Record<ViewportBreakpoint, number> {
  blue: string;
  cyan: string;
  green: string;
  orange: string;
  purple: string;
  red: string;
  yellow: string;

  blueContrast: string;
  cyanContrast: string;
  greenContrast: string;
  orangeContrast: string;
  purpleContrast: string;
  redContrast: string;
  yellowContrast: string;

  backgroundColor: string;

  borderRadius: string;
  borderRadiusRound: string;

  fontSize: number;

  gap: number;

  linkActiveBorderColor: string;
  linkActiveColor: string;
  linkColor: string;
  linkColorContrast: string;
  linkFocusBorderColor: string;
  linkFocusColor: string;
  linkHoverBorderColor: string;
  linkHoverColor: string;
  linkVisitedColor: string;

  textColor: string;
  textLightColor: string;
  textStrongColor: string;
}

const contrastColor = (color: string) => (getLuminance(color) > 0.55 ? hsla(0, 0, 0, 0.7) : hsl(0, 0, 1));

const grayDarker = hsl(0, 0, 0.18);
const grayDark = hsl(0, 0, 0.29);
const gray = hsl(0, 0, 0.54);
const grayLight = hsl(0, 0, 0.71);
const grayLighter = hsl(0, 0, 0.86);
const grayLightest = hsl(0, 0, 0.96);

const red = hsl(3, 0.85, 0.61);
const orange = hsl(23, 0.9, 0.55);
const yellow = hsl(48, 1, 0.67);
const green = hsl(98, 0.43, 0.55);
const cyan = hsl(204, 0.86, 0.53);
const blue = hsl(220, 1, 0.59);
const purple = hsl(271, 1, 0.71);

const blueContrast = contrastColor(blue);

const light = grayLightest;
const dark = grayDarker;
const primary = blue;
const secondary = grayLighter;
const tertiary = orange;
const info = cyan;
const success = green;
const warning = yellow;
const danger = red;

export const variantContrasts: Record<Variant, ColorContrast> = {
  light: "lightContrast",
  dark: "darkContrast",
  primary: "primaryContrast",
  secondary: "secondaryContrast",
  tertiary: "tertiaryContrast",
  info: "infoContrast",
  success: "successContrast",
  warning: "warningContrast",
  danger: "dangerContrast",
};

const theme: Theme = {
  black: hsl(0, 0, 0.04),

  grayDarker,
  grayDark,
  gray,
  grayLight,
  grayLighter,
  grayLightest,

  white: hsl(0, 0, 1),

  blue,
  cyan,
  green,
  orange,
  purple,
  red,
  yellow,

  blueContrast,
  cyanContrast: contrastColor(cyan),
  greenContrast: contrastColor(green),
  orangeContrast: contrastColor(orange),
  purpleContrast: contrastColor(purple),
  redContrast: contrastColor(red),
  yellowContrast: contrastColor(yellow),

  light,
  dark,
  primary,
  secondary,
  tertiary,
  info,
  success,
  warning,
  danger,

  lightContrast: dark,
  darkContrast: light,
  primaryContrast: contrastColor(primary),
  secondaryContrast: contrastColor(secondary),
  tertiaryContrast: contrastColor(tertiary),
  infoContrast: contrastColor(info),
  successContrast: contrastColor(success),
  warningContrast: contrastColor(warning),
  dangerContrast: contrastColor(danger),

  backgroundColor: hsl(239, 0.33, 0.98),

  borderRadius: "3px",
  borderRadiusRound: "9999px",

  fontSize: 10,

  gap: 20,
  small: 576,
  medium: 768,
  large: 992,
  extraLarge: 1200,

  linkColor: blue,
  linkColorContrast: blueContrast,
  linkVisitedColor: purple,
  linkHoverColor: grayDarker,
  linkHoverBorderColor: grayLight,
  linkFocusColor: grayDarker,
  linkFocusBorderColor: blue,
  linkActiveColor: grayDarker,
  linkActiveBorderColor: grayDark,

  textColor: grayDark,
  textLightColor: gray,
  textStrongColor: grayDarker,
};

export default theme;
