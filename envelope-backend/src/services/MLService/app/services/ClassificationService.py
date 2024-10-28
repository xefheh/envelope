from transformers import pipeline

class ClassificationService:
    def __init__(self):
        self.classifier = pipeline("zero-shot-classification", model="joeddav/xlm-roberta-large-xnli")

    def classifyText(self, text: str, candidateLabels: list, threshold: float = 0.250):
        result = self.classifier(text, candidateLabels)
        filteredLabels = [label for label, score in zip(result['labels'], result['scores']) if score >= threshold]
        return filteredLabels
