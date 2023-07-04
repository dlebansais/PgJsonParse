namespace Translator;

using System;
using System.Collections;
using System.Collections.Generic;
using PgObjects;

public class EnumStringMap
{
    public static readonly Dictionary<RecipeEffect, string> RecipeEffectTable = new Dictionary<RecipeEffect, string>()
    {
        { RecipeEffect.CreateGeologySurveyRedwall_GeologySurveySerbule0, "CreateGeologySurveyRedwall(GeologySurveySerbule0)" },
        { RecipeEffect.CreateGeologySurveyBlue_GeologySurveySerbule1, "CreateGeologySurveyBlue(GeologySurveySerbule1)" },
        { RecipeEffect.CreateGeologySurveyGreen_GeologySurveySerbule2, "CreateGeologySurveyGreen(GeologySurveySerbule2)" },
        { RecipeEffect.CreateGeologySurveyWhite_GeologySurveySerbule4, "CreateGeologySurveyWhite(GeologySurveySerbule4)" },
        { RecipeEffect.CreateGeologySurveyRedwall_GeologySurveySouthSerbule0, "CreateGeologySurveyRedwall(GeologySurveySouthSerbule0)" },
        { RecipeEffect.CreateGeologySurveyOrange_GeologySurveySouthSerbule3, "CreateGeologySurveyOrange(GeologySurveySouthSerbule3)" },
        { RecipeEffect.CreateGeologySurveyWhite_GeologySurveySouthSerbule4, "CreateGeologySurveyWhite(GeologySurveySouthSerbule4)" },
        { RecipeEffect.CreateGeologySurveyBlue_GeologySurveyEltibule1, "CreateGeologySurveyBlue(GeologySurveyEltibule1)" },
        { RecipeEffect.CreateGeologySurveyGreen_GeologySurveyEltibule2, "CreateGeologySurveyGreen(GeologySurveyEltibule2)" },
        { RecipeEffect.CreateGeologySurveyOrange_GeologySurveyEltibule3, "CreateGeologySurveyOrange(GeologySurveyEltibule3)" },
        { RecipeEffect.CreateGeologySurveyBlue_GeologySurveyKurMountains1, "CreateGeologySurveyBlue(GeologySurveyKurMountains1)" },
        { RecipeEffect.CreateGeologySurveyGreen_GeologySurveyKurMountains2, "CreateGeologySurveyGreen(GeologySurveyKurMountains2)" },
        { RecipeEffect.CreateGeologySurveyOrange_GeologySurveyKurMountains3, "CreateGeologySurveyOrange(GeologySurveyKurMountains3)" },
        { RecipeEffect.CreateGeologySurveyWhite_GeologySurveyKurMountains4, "CreateGeologySurveyWhite(GeologySurveyKurMountains4)" },
    };

    public static Dictionary<Type, IDictionary> Tables { get; } = new Dictionary<Type, IDictionary>()
    {
        { typeof(RecipeEffect), RecipeEffectTable },
    };
}
